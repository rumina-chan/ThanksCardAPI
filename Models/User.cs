namespace ThanksCardAPI.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        // 多対1: User エンティティは1つの DepartmentChildren エンティティに属する
        public long? DepartmentChildrenId { get; set; }
        public virtual DepartmentChildren DepartmentChildren { get; set; }
    }
}
