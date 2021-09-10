namespace SimpleCodingChallenge.Common.DTO
{
    public class EmployeeDto
    {
        public string EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string JobTitle { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + (string.IsNullOrEmpty(FirstName) ? null : " ") + LastName;
            }
        }
    }
}
