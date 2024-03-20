using Dapper;
using infrastructure.db_connection;
using infrastructure.entity;

namespace infrastructure.repositories
{
    public class ImageRepository
    {
        public readonly ConnectionManager _Connection;

        public ImageRepository(ConnectionManager connection)
        {
            _Connection = connection;
        }
        public IEnumerable<AiImages> GetAll()
        {
            var sql = "SELECT * FROM ai_images";

            using (var con = _Connection.GetConnection())
            {
                return con.Query<AiImages>(sql);
            }
        }
        public AiImages GetById(int imageId)
        {
            var sql = "SELECT * FROM ai_images WHERE imageid = @ImageId";

            using (var con = _Connection.GetConnection())
            {
                return con.QueryFirstOrDefault<AiImages>(sql, new { ImageId = imageId });
            }
        }

        public IEnumerable<AiImages> GetByCategory(String category)
        {
            var sql = "SELECT * FROM ai_images WHERE category = @category";

            using (var con = _Connection.GetConnection())
            {
                return con.Query<AiImages>(sql, new { Category = category });
            }
        }
        
        public void Create(AiImages aiImages)
        {
            var sql = "INSERT INTO ai_images (category, imageurl) VALUES (@Category, @ImageUrl)";

            using (var con = _Connection.GetConnection())
            {
                con.Execute(sql, new {category = aiImages.Category, imageurl = aiImages.ImageUrl });
            }
        }
        public void Update(AiImages aiImages)
        {
            var sql = "UPDATE ai_images SET category = @Category, imageurl = @ImageUrl WHERE imageid = @ImageId";

            using (var con = _Connection.GetConnection())
            {
                con.Execute(sql, new {Category = aiImages.Category, ImageUrl = aiImages.ImageUrl, ImageId = aiImages.ImageId });
            }
        }
        
        public void Delete(int imageId)
        {
            var sql = "DELETE FROM ai_images WHERE imageid = @ImageId";

            using (var con = _Connection.GetConnection())
            {
                con.Execute(sql, new { ImageId = imageId });
            }
        }
        
        
        
    }
}
