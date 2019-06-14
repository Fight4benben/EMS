using EMS.DAL.Entities;
using EMS.DAL.Entities.Setting;
using EMS.DAL.IRepository.Setting;
using EMS.DAL.StaticResources.Setting;
using EMS.DAL.ViewModels.Setting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.RepositoryImp.Setting
{
    public class SvgSettingContext : ISvgSettingContext
    {
        private EnergyDB _db = new EnergyDB();

        public Svg GetSvgById(string svgId)
        {
            return _db.Svg.Where(s => s.SvgId == svgId).ToList().First();
        }

        public SvgBinding GetSvgBindingBySvgId(string svgId)
        {
            List<SvgBinding> list = _db.SvgBinding.Where(s => s.SvgId == svgId).ToList();

            if (list.Count > 0)
                return list.First();
            else
                return null;
        }

        public List<SvgViewModel> GetSvgViewModels(string buildId)
        {
            return _db.Database.SqlQuery<SvgViewModel>(SvgSettingResources.SvgListSQL, new SqlParameter("@BuildId", buildId)).ToList();
        }

        public int Insert(Svg svg)
        {
            _db.Svg.Add(svg);
            return _db.SaveChanges();
        }

        public int Update(Svg svg)
        {
            _db.Entry(svg).State = System.Data.Entity.EntityState.Modified;

            _db.Entry(svg).Property(s => s.SvgName).IsModified = true;
            return _db.SaveChanges();
        }



        public int Delete(string svgId)
        {
            Svg svg = GetSvgById(svgId);
            SvgBinding svgBinding = GetSvgBindingBySvgId(svgId);
            int count = 0;

            using (DbContextTransaction trans = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.Svg.Remove(svg);
                    count += _db.SaveChanges();

                    _db.SvgBinding.Remove(svgBinding);
                    count += _db.SaveChanges();

                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                }
            }

           return count;
        }

        public List<Identify> GetMeters(string buildId)
        {
            return _db.Database.SqlQuery<Identify>(SvgSettingResources.CircuitsSQL, new SqlParameter("@BuildId",buildId)).ToList();
           
        }

        public List<Identify> GetParams(string buildId)
        {
            return _db.Database.SqlQuery<Identify>(SvgSettingResources.ParamsSQL, new SqlParameter("@BuildId", buildId)).ToList();
        }

        public int InsertSvgBinding(SvgBinding binding)
        {
            _db.SvgBinding.Add(binding);

            return _db.SaveChanges();
        }

        public int UpdateSvgBinding(SvgBinding binding)
        {
            SqlParameter[] sqlParameters = {
                new SqlParameter("@Meters",binding.Meters),
                new SqlParameter("@Params",binding.Params),
                new SqlParameter("@Id",binding.ID)
            };
            return _db.Database.ExecuteSqlCommand(SvgSettingResources.UpdateSvgBindingSQL,sqlParameters);
            
        }
    }
}
