namespace ThanksCardAPI.Models
{
    public class DepartmentChildren
    {
        public long Id { get; set; }
        public string Name { get; set; }


        // 多対1: DepartmentChildrenエンティティは1つの Department エンティティに属する
        public long? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
