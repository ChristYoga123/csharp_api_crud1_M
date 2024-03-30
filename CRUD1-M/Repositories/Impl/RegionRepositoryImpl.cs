using CRUD1_M.Entities;
using CRUD1_M.Utils;
using Npgsql;

namespace CRUD1_M.Repositories.Impl
{
    public class RegionRepositoryImpl : GenericRepository<Region, int>
    {
        private DbUtil dbUtil;
        public RegionRepositoryImpl(DbUtil dbUtil)
        {
            this.dbUtil = dbUtil;
        }

        public List<Region> findAll()
        {
            List<Region> regions = new List<Region>();
            string sql = "SELECT * FROM regions";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Region region = new Region();
                    region.id = reader.GetInt32(0);
                    region.name = reader.GetString(1);
                    regions.Add(region);
                }
                cmd.Dispose();
                dbUtil.closeConnection();
            }catch(Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return regions;
        }

        public Region findById(int id)
        {
            string sql = "SELECT * FROM regions WHERE id = @id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Region region = new Region();
                    region.id = reader.GetInt32(0);
                    region.name = reader.GetString(1);
                    cmd.Dispose();
                    dbUtil.closeConnection();
                    return region;
                }
                cmd.Dispose();
                dbUtil.closeConnection();
            }catch(Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return null;
        }

        public Region create(Region entity)
        {
            string sql = "INSERT INTO regions (name) VALUES (@name) RETURNING id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@name", entity.name);
                entity.id = (int)cmd.ExecuteScalar();
                cmd.Dispose();
                dbUtil.closeConnection();
                return entity;
            }catch(Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return null;
        }

        public Region update(Region entity)
        {
            string sql = "UPDATE regions SET name = @name WHERE id = @id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@name", entity.name);
                cmd.Parameters.AddWithValue("@id", entity.id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dbUtil.closeConnection();
                return entity;
            } catch(Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return null;
        }

        public Region delete(Region entity)
        {
            string sql = "DELETE FROM regions WHERE id = @id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@id", entity.id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dbUtil.closeConnection();
                return entity;
            }catch(Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return null;
        }
    }
}
