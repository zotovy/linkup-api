using System;
using System.IO;

namespace Services.Media {
    public sealed class MediaService: IMediaService {
        public void SaveUserProfilePicture(byte[] image, string filename) {
            var projectPath = Directory.GetCurrentDirectory();
            var imagePath = projectPath + "/static/profile-image/" + filename;
            File.WriteAllBytes(imagePath, image);
        }

        public void DeleteUserProfilePicture(string filename) {
            var projectPath = Directory.GetCurrentDirectory();
            var imagePath = projectPath + "/static/profile-image/" + filename;
            File.Delete(imagePath);
        }

        public bool CheckExistUserProfilePicture(string filename) {
            var projectPath = Directory.GetCurrentDirectory();
            var imagePath = projectPath + "/static/profile-image/" + filename;
            return File.Exists(imagePath);
        }
    }
}