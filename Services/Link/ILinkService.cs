namespace Services.Link {
    public interface ILinkService {
        public int Add(Domain.Link link);
        public void ChangeTo(Domain.Link link);
        public int? GetAuthorIdOf(int id);
    }
}