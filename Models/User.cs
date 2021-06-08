namespace ThanksCardAPI.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        // ����1: User �G���e�B�e�B��1�� DepartmentChildren �G���e�B�e�B�ɑ�����
        public long? DepartmentChildrenId { get; set; }
        public virtual DepartmentChildren DepartmentChildren { get; set; }
    }
}
