using System.Threading.Tasks;

namespace Services.Media {
    public interface IMediaService {
        public void SaveUserProfilePicture(byte[] image, string filename);
        public void DeleteUserProfilePicture(string filename);
        public bool CheckExistUserProfilePicture(string filename);
    }
}