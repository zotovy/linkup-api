namespace Domain {
    public sealed class Ref<T> {
        public int Id;
        public T? Model;

        public Ref(int id, T? model) {
            Id = id;
            if (model != null) Model = model;
        }

        public Ref(int id) {
            Id = id;
        }

        public void OverlookModel() => Model = default;

        public bool IsPopulated() => Model != null;
    }
}