namespace Services.Link {
    public interface ILinkService {
        public void Add(Domain.Link link);
        public void ChangeTo(Domain.Link link);
    }
}