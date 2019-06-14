using EMS.DAL.Entities.Setting;
using EMS.DAL.IRepository;
using EMS.DAL.IRepository.Setting;
using EMS.DAL.RepositoryImp;
using EMS.DAL.RepositoryImp.Setting;
using EMS.DAL.ViewModels.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Services.Setting
{
    public class SvgSettingService
    {
        IHomeDbContext homeContext = new HomeDbContext();
        ISvgSettingContext context = new SvgSettingContext();
        public SvgSettingViewModel GetByName(string userName)
        {
            SvgSettingViewModel viewModel = new SvgSettingViewModel();
            viewModel.Builds = homeContext.GetBuildsByUserName(userName);
            string buildId = "";
            if (viewModel.Builds.Count > 0)
                buildId = viewModel.Builds.First().BuildID;
            else
                buildId = "";
            viewModel.Svgs = context.GetSvgViewModels(buildId);
            
            return viewModel;
        }

        public SvgSettingViewModel GetByBuildId(string buildId)
        {
            SvgSettingViewModel viewModel = new SvgSettingViewModel();
            viewModel.Svgs = context.GetSvgViewModels(buildId);

            return viewModel;
        }

        public SvgBindingViewModel GetBindingViewModel(string buildId,string svgId)
        {
            SvgBindingViewModel model = new SvgBindingViewModel();
            model.MeterList = context.GetMeters(buildId);
            model.ParamList = context.GetParams(buildId);
            model.SelectedMeters = new List<string>();
            model.SelectedParams = new List<string>();

            SvgBinding binding = context.GetSvgBindingBySvgId(svgId);
            if (binding != null)
            {
                if (binding.Meters.Length > 0)
                    model.SelectedMeters.AddRange(binding.Meters.Split('|'));

                if (binding.Params.Length > 0)
                    model.SelectedParams.AddRange(binding.Params.Split('|'));
            }

            return model;
        }

        public object UpdateSvgBinding(string svgId, string postMeters, string postParams)
        {
            SvgBinding binding = new SvgBinding();
            binding.ID = 0;
            binding.SvgId = svgId;
            binding.Meters = postMeters;
            binding.Params = postParams;

            SvgBinding tempBinding = context.GetSvgBindingBySvgId(svgId);
            int count = 0;
            if (tempBinding == null)
                count= context.InsertSvgBinding(binding);
            else
            {
                binding.ID = tempBinding.ID;
                
                count = context.UpdateSvgBinding(binding);
            }

                

            if (count > 0)
                return new { Flag = true, Message = "一次图绑定信息成功！" };
            else
                return new { Flag = false, Message = "一次图绑定信息失败！" };
        }

        public object Insert(string buildId,string svgName)
        {
            if (string.IsNullOrEmpty(svgName))
            {
                return new { Error = "一次图名称不允许为空，请检查输入内容！" };
            }
            string svgId = "";
            List<SvgViewModel> svgs = context.GetSvgViewModels(buildId);
            if (svgs.Count > 0)
            {
                string temp = svgs.Last().SvgId;
                int index = Convert.ToInt32(temp.Substring(temp.Length - 4));
                index++;

                svgId = buildId + index.ToString().PadLeft(4, '0');
            }
            else
            {
                svgId = buildId + "0001";
            }
            Svg svg = new Svg()
            {
                BuildId = buildId,
                SvgId = svgId,
                SvgName = svgName,
                Path = svgId + ".svg"
            };

            int count = context.Insert(svg);

            if (count > 0)
                return new { Flag = true, Message = "一次图信息创建成功，可以上传对应一次图文件！" };
            else
                return new { Flag = false, Message = "一次图信息创建失败，请检查原因！" };
        }

        public object Update(string svgId, string svgName)
        {
            Svg svg =  context.GetSvgById(svgId);

            svg.SvgName = svgName;
            int count = context.Update(svg);

            if (count > 0)
                return new { Flag = true, Message = "成功修改一次图名称！" };
            else
                return new { Flag = false, Message = "未提交修改！" };
        }

        public object Delete(string svgId)
        {
            int count = context.Delete(svgId);

            if (count > 0)
                return new { Flag = true, Message = "删除一次图配置！" };
            else
                return new { Flag = false, Message = "删除一次图失败，请稍后再试，或者联系管理员！" };
        }
    }
}
