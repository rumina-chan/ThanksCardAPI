namespace ThanksCardAPI.Models
{
    public class DepartmentChildren
    {
        public long Id { get; set; }
        public string Name { get; set; }


        // ����1: DepartmentChildren�G���e�B�e�B��1�� Department �G���e�B�e�B�ɑ�����
        public long? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
