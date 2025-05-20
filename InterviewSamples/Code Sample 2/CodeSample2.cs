using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Http;

namespace InterviewSamples
{
    [RoutePrefix("api/v1/sample2/user")]
    public class Sample2ApiController : ApiController
    {
        private readonly ISample2Manager _manager;
        private readonly ISample2Repository _repository;
        private readonly ISample2Emailer _emailer;

        public Sample2ApiController(ISample2Manager manager, ISample2Repository repository, ISample2Emailer emailer)
        {
            _manager = manager;
            _repository = repository;
            _emailer = emailer;
        }

        [HttpPost]
        [Authorize]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            var user = _manager.GetAllUsers().FirstOrDefault(r => r.Id == id);

            return Ok(user);
        }

        [HttpPut]
        [AllowAnonymous]
        [Route]
        public IHttpActionResult Add(Sample2UserModel user)
        {
            user.Id = (new Sample2Manager()).AddUser(user);

            return Ok(user);
        }

        [Authorize]
        public IHttpActionResult Update(int id, Sample2UserModel user)
        {
            var repository = new Sample2Repository();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = repository.GetUserById(id);

            entity.Name = user.Name;
            entity.PhoneNumber = user.PhoneNumber;
            entity.Email = user.Email;

            repository.Update(id, entity);

            return Ok(new { Id = id });
        }

        [HttpPost]
        [Authorize]
        [Route("sendEmail")]
        public IHttpActionResult SendEmail([FromBody]string emailType, [FromBody] Sample2UserModel user)
        {
            var enumType = Enum.Parse(typeof(EmailType), emailType);
            if(enumType == EmailType.NewUser)
            {
                _emailer.SendNewUserEmail(user);
            } 
            else if (enumType == EmailType.PasswordChanged)
            {
                _emailer.SendPasswordChangedEmail(user);
            }
            else if (enumType == EmailType.Overdue)
            {
                _emailer.SendOverdueEmail(user);
            }

            return Ok();

        }
    }


    // Write the user class for the code above 
    public class Sample2UserModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$")]
        public string PhoneNumber { get; set; }
        [MaxLength(255)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }
    }

    // Write an interface for Sample2Manager
    public interface ISample2Manager
    {
        Sample2UserModel GetUserById(int id);
        List<Sample2UserModel> GetAllUsers();
        int AddUser(Sample2UserModel user);
    }

    public class Sample2Manager : ISample2Manager
    {
        public int AddUser(Sample2UserModel user)
        {
            throw new System.NotImplementedException();
        }

        public List<Sample2UserModel> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public Sample2UserModel GetUserById(int id)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Sample2UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
    }

    public interface ISample2Repository
    {
        Sample2UserEntity GetUserById(int id);
        List<Sample2UserModel> GetAllUsers();
        bool Update(int id, Sample2UserEntity user);
        bool HasUser(int id);
    }

    public class Sample2Repository : ISample2Repository
    {
        public List<Sample2UserModel> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public Sample2UserEntity GetUserById(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool HasUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(int id, Sample2UserEntity user)
        {
            throw new System.NotImplementedException();
        }
    }


    public interface ISample2Emailer
    {
        public void SendOverdueEmail(Sample2UserModel model);
        public void SendNewUserEmail(Sample2UserModel model);
        public void SendPasswordChangedEmail(Sample2UserModel model);
    }

    public enum EmailType
    {
        Overdue,
        NewUser,
        PasswordChanged
    }
}
