using CRUD1_M.Entities;
using CRUD1_M.Utils;
using Npgsql;

namespace CRUD1_M.Repositories.Impl
{
    public class CountryRepositoryImpl : GenericRepository<Country, int>
    {
        private DbUtil dbUtil;
        public CountryRepositoryImpl(DbUtil dbUtil)
        {
            this.dbUtil = dbUtil;
        }
        public List<Country> findAll()
        {
            List<Country> list = new List<Country>();
            string sql = "SELECT country.id, country.name, region.id, region.name FROM countries country INNER JOIN regions region ON country.region_id = region.id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Country country = new Country();
                    country.id = reader.GetInt32(0);
                    country.name = reader.GetString(1);
                    Region region = new Region();
                    region.id = reader.GetInt32(2);
                    region.name = reader.GetString(3);
                    country.region = region;
                    list.Add(country);
                }
                cmd.Dispose();
                dbUtil.closeConnection();
            } catch(Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return list;
        }

        public Country findById(int id)
        {
            string sql = "SELECT * FROM countries WHERE id = @id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Country country = new Country();
                    country.id = reader.GetInt32(0);
                    country.name = reader.GetString(1);
                    cmd.Dispose();
                    dbUtil.closeConnection();
                    return country;
                }
                cmd.Dispose();
                dbUtil.closeConnection();
            } catch(Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return null;
        }

        public Country create(Country country)
        {
            string sql = "INSERT INTO countries(name, region_id) VALUES(@name, @region_id)";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@name", country.name);
                cmd.Parameters.AddWithValue("@region_id", country.region.id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dbUtil.closeConnection();
            } catch(Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return country;
        }

        public Country update(Country country)
        {
            string sql = "UPDATE countries SET name = @name, region_id = @region_id WHERE id = @id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@name", country.name);
                cmd.Parameters.AddWithValue("@region_id", country.region.id);
                cmd.Parameters.AddWithValue("@id", country.id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dbUtil.closeConnection();
            } catch(Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return country;
        }

        public Country delete(Country country)
        {
            string sql = "DELETE FROM countries WHERE id = @id";
            try
            {
                NpgsqlCommand cmd = dbUtil.GetNpgsqlCommand(sql);
                cmd.Parameters.AddWithValue("@id", country.id);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                dbUtil.closeConnection();
            } catch(Exception e)
            {
                dbUtil.closeConnection();
                throw new Exception(e.Message);
            }
            return country;
        }
    }
}
