namespace DAISUni2.ViewModels.TeacherViewModels
{
    public class TeacherViewModel
    {
        
        public int Id { get; set; }
     
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
        
        public string Email { get; set; }

        public DateTime HiredOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid? UsersId { get; set; }
    }
}
