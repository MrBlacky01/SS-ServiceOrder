using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.AlbumViewModels;
using ServiceOrder.ViewModel.ViewModels.Implementation.PhotoViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class AlbumService : IAlbumService
    {
        private IUnitOfWork DataBase { get; set; }
        private IMapper _mapper;

        public AlbumService(IUnitOfWork dataBase, IMapper mapper)
        {
            DataBase = dataBase;
            _mapper = mapper;
        }


        public void Add(AlbumViewModel item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int? id)
        {
            if(id == null) return;
            DataBase.Albums.Delete(id);
            DataBase.Save();
        }

        public void Update(AlbumViewModel item)
        {
            DataBase.Albums.Update(_mapper.Map<AlbumViewModel,Album>(item));
            DataBase.Save();
        }

        public AlbumViewModel Get(int? id)
        {
            if (id == null) return null;
            var album = DataBase.Albums.Get((int) id);
            return album == null ? null : _mapper.Map<Album, AlbumViewModel>(album);
        }

        public IEnumerable<AlbumViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }

        public void AddPhoto(int albumId, PhotoViewModel photo)
        {
            var album = FindAlbum(albumId);
            var mapedPhoto = _mapper.Map<PhotoViewModel, Photo>(photo);
            album.AlbumPhotos.Add(mapedPhoto);
            DataBase.Albums.Update(album);
            DataBase.Save();
        }

        public void AddPhotosList(int albumId, List<PhotoViewModel> photos)
        {
            var album = FindAlbum(albumId);
            var mapedPhotos = _mapper.Map<IEnumerable<PhotoViewModel>, IEnumerable<Photo>>(photos);
            album.AlbumPhotos.AddRange(mapedPhotos);
            DataBase.Albums.Update(album);
            DataBase.Save();
        }

        public IEnumerable<AlbumViewModel> GetProviderAlbums(string providerId)
        {
            var albums = DataBase.Albums.Find(src => src.Provider.UserId == providerId);
            return _mapper.Map<IEnumerable<Album>, IEnumerable<AlbumViewModel>>(albums);
        }

        public void Add(ShortAlbumViewModel item)
        {
            if (DataBase.Albums.Find(src => src.Provider.UserId == item.ServiceProviderId)
                .All(src => src.Title != item.Title))
            {
                DataBase.Albums.Create(_mapper.Map<ShortAlbumViewModel,Album>(item));
                DataBase.Save();
            }
            else
            {
                throw new Exception("This provider already has album with such name");
            }
        }

        private Album FindAlbum(int albumId)
        {
            var album = DataBase.Albums.Find(src => src.Id == albumId).FirstOrDefault();
            if (album == null)
            {
                throw new Exception("No such album");
            }
            else
            {
                return album;
            }
        }

        public void UploadAndShowResults(HttpContextBase contentBase, List<AbstractDataUploadResult> resultList,int albumId)
        {
            var httpRequest = contentBase.Request;

            foreach (var headers in from object file in httpRequest.Files select httpRequest.Headers)
            {
                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {

                    UploadWholeFile(contentBase, resultList,albumId);
                }
                else
                {

                    UploadPartialFile(headers["X-File-Name"], contentBase, resultList, albumId);
                }
            }
        }

        public List<ViewDataUploadFilesResult> GetPhotosList(int? albumId)
        {
            var album = Get(albumId);
            if (album == null)
            {
                throw new Exception("No such album");
            }
            var result = album.AlbumPhotos.Select(file => UploadResult(file.FileName, Convert.FromBase64String(file.PhotoImage).Length, file.FileName, file.Id)).ToList();
            return result;
        }


        private void UploadWholeFile(HttpContextBase requestContext, List<AbstractDataUploadResult> statuses,int albumId)
        { 
            var request = requestContext.Request;
            for (var i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];
                if (file == null) continue;
                try
                {
                    if (CheckPhotoFileNameInAlbum(file.FileName, albumId))
                    {
                        throw new FileLoadException("Can't be the same names of photos in album");
                    }
                    byte[] imageBytes = new byte[file.InputStream.Length];
                    file.InputStream.Read(imageBytes, 0, (int)file.InputStream.Length);
                    AddPhoto(albumId, new PhotoViewModel()
                    {
                        ContentType = file.ContentType,
                        FileName = file.FileName,
                        ImageBytes = imageBytes

                    });
                    var photo =
                        DataBase.Photos.Find(src => src.PhotoAlbum.Id == albumId && src.FileName == file.FileName).FirstOrDefault();
                    if (photo == null)
                    {
                        statuses.Add(UploadErrorResult(file.FileName, file.ContentLength, "Wrong additing to database"));
                    }
                    else
                    {
                        statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName, photo.Id));
                    }
                    
                }
                catch (Exception exception)
                {                 
                    statuses.Add(UploadErrorResult(file.FileName, file.ContentLength,exception.Message));
                }
                
                
            }
        }



        private void UploadPartialFile(string fileName, HttpContextBase requestContext, List<AbstractDataUploadResult> statuses, int albumId)
        {
            var request = requestContext.Request;
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");           
            var file = request.Files[0];
            var inputStream = file.InputStream;

            try
            {
                
                var storageImage = DataBase.Photos.Find(src => src.PhotoAlbum.Id == albumId && src.FileName.Equals(file.FileName))
                    .FirstOrDefault();
                if (storageImage == null)
                {
                    throw new Exception("Attempt to upload missing file");
                }

                var storageImageBytes = Convert.FromBase64String(storageImage.PhotoImage);
                Array.Resize(ref storageImageBytes, storageImageBytes.Length + (int)inputStream.Length);
                inputStream.Read(storageImageBytes, storageImageBytes.Length, (int)inputStream.Length);
                storageImage.PhotoImage = Convert.ToBase64String(storageImageBytes);
                DataBase.Photos.Update(storageImage);

                statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName,storageImage.Id));
            }
            catch (Exception exception)
            {

                statuses.Add(UploadErrorResult(file.FileName, file.ContentLength, exception.Message));
            }
            
        }

        private bool CheckPhotoFileNameInAlbum(string fileName, int albumId)
        {
            var album = Get(albumId);
            return album.AlbumPhotos.Any(src => src.FileName == fileName);
        }

        public ViewDataUploadFilesResult UploadResult(string FileName, int fileSize, string FileFullPath,int id)
        {
            string getType =MimeMapping.GetMimeMapping(FileFullPath);
            var result = new ViewDataUploadFilesResult()
            {
                name = FileName,
                size = fileSize,
                type = getType,
                url =  "/Photo/Get/?photoId="+id,
                deleteUrl = "/Photo/Delete/?photoId=" + id,    
                deleteType = "GET",
                thumbnailUrl = "/Photo/Get/?photoId=" + id
            };
            return result;
        }

        public ErrorDataUploadResult UploadErrorResult(string FileName, int fileSize, string errorMessage)
        {
            return new ErrorDataUploadResult()
            {
                name = FileName,
                size = fileSize,
                error = errorMessage
            };
        }
    }
}
