using Dapper;
using infrastructure.db_connection;
using infrastructure.entity;

namespace infrastructure.repositories
{
    public class ImageRepository
    {
        public readonly ConnectionManager _Connection;

        public IEnumerable<AiImages> GetAll()
        {
            var sql = "SELECT * FROM ai_images";

            using (var con = _Connection.GetConnection())
            {
                con.Open();
                return con.Query<AiImages>(sql);
            }
        }
        
        public AiImages GetById(int imageId)
        {
            var sql = "SELECT * FROM ai_images WHERE imageid = @ImageId";

            using (var con = _Connection.GetConnection())
            {
                con.Open();
                return con.QueryFirstOrDefault<AiImages>(sql, new { ImageId = imageId });
            }
        }
        
        public void Create(AiImages aiImages)
        {
            var sql = "INSERT INTO ai_images (category, imageurl) VALUES (@Category, @ImageUrl)";

            using (var con = _Connection.GetConnection())
            {
                con.Open();
                con.Execute(sql, new {category = aiImages.Category, imageurl = aiImages.ImageUrl });
            }
        }
        public void Update(AiImages aiImages)
        {
            var sql = "UPDATE ai_images SET category = @Category, imageurl = @ImageUrl WHERE imageid = @ImageId";

            using (var con = _Connection.GetConnection())
            {
                con.Open();
                con.Execute(sql, aiImages);
            }
        }
        
        public void Delete(int imageId)
        {
            var sql = "DELETE FROM ai_images WHERE imageid = @ImageId";

            using (var con = _Connection.GetConnection())
            {
                con.Open();
                con.Execute(sql, new { ImageId = imageId });
            }
        }
        
        
        
    }
}
