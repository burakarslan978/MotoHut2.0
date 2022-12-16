using Microsoft.AspNetCore.Mvc.Rendering;

namespace MotoHut2._0.Models
{
    public class ViewModel
    {
        public List<MotorViewModel>? MotorModels { get; set; }
        public List<HuurderMotorViewModel>? HuurderMotorModels { get; set; }
        public int[]? MotorId { get; set; }
        public IEnumerable<SelectListItem>? Motors { get; set; }
        public MotorViewModel? MotorViewModel { get; set; }
        public HuurderMotorViewModel? HuurderMotorViewModel { get; set; }
        public UserModel? UserModel { get; set; }
    }
}
