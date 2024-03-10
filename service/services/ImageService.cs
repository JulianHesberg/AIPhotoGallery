using infrastructure.entity;
using infrastructure.repositories;

namespace service.services
{
    public class ImageService
    {
        private readonly ImageRepository _repository;

        public ImageService(ImageRepository imageRepository)
        {
            _repository = imageRepository;
        }

        public IEnumerable<AiImages> GetAllImages()
        {
            return _repository.GetAll();
        }

        public AiImages GetImageById(int id)
        { 
            return _repository.GetById(id);
        }

        public void CreateImage(AiImages image)
        {
            if (image == null)
            {
                throw new ArgumentException(nameof(image));
            }
            _repository.Create(image);
        }

        public void UpdateImage(AiImages image)
        {
            if (image == null)
            {
                throw new AggregateException(nameof(image));
            }
            _repository.Update(image);
        }

        public void DeleteImage(int id)
        {
            _repository.Delete(id);
        }

    }
}
