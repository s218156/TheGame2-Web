using System.ComponentModel.DataAnnotations;

namespace TheGame2_Frontend.Models
{
    public class SignUpModel
    {
        public string username { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DataType(DataType.Password)]
        public string passwordConfirm { get; set; }
        public string fullname { get; set; }
    }
}
