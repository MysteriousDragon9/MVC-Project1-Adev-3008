using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BITCollege_AP_2.Models
{
    
    /// <summary>
    /// Student Model , Represent the Student Table in the base
    /// </summary>
    public class Student

    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]

        [ForeignKey("GradPointState")]
        public int GradePointStatedId { get; set; }


        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        [Required]
        [Range(10000000, 99999999)]
        [Display(Name = "Student\nNumber")]
        public long StudentNumber { get; set; }


        [Required]
        [Display(Name = "First\nName")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "Last\nName")]
        public String LastName { get; set; }

        [Required]
        public String Address { get; set; }

        [Required]
        public String City { get; set; }

        [Required]
        [StringLength(7)]
        [RegularExpression("^(N[BLSTU]|[AMN]B|[BQ]C|ON|PE|SK|YT)")]
        public String Province { get; set; }


        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateCreated { get; set; }



        [Display(Name = "Grade\nPoint\nAverage")]
        [Range(0, 4.5)]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public double? GradePointAverage { get; set; }

        [Required]
        [Display(Name = "Fees")]
        [DisplayFormat(DataFormatString = "{0:c2}")]
        public double OutstandingFees { get; set; }

        public string Notes { get; set; }


        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }

        [Display(Name = "Address")]
        public string FullAddress
        {
            get
            {
                return string.Format("{0} {1} {2} ", Address, City, Province);
            }
        }

        //navigational Properties

        public virtual GradePointState GradePointState { get; set; }
        public virtual AcademicProgram AcademicProgram { get; set; }

        public virtual ICollection<Registration> Registration { get; set; }

    }


    /// <summary>
    ///  AcademicProgram Model , Represent the AcademicProgram Table in the base
    /// </summary>
    public class AcademicProgram
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AcademicProgramId { get; set; }

        [Required]
        [Display(Name = "Program")]
        public string ProgramAcronym { get; set; }

        [Required]
        [Display(Name = "Program\nName")]
        public string Description { get; set; }


        //navigational Properties
        public virtual ICollection<Student> Student { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }


    /// <summary>
    /// Registration Model , Represent theRegistration Table in the base
    /// </summary>
    public class Registration
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RegistrationId { get; set; }

        [Required]
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }


        [Required]
        [Display(Name = "Registration\nNumber")]
        public long RegistatrationNumber { get; set; }


        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime RegistrationDate { get; set; }


        [DisplayFormat(NullDisplayText = "[Ungraded]")]
        [Range(0, 1)]
        public double? Grade { get; set; }

        public string Notes { get; set; }



        //navigational Properties
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }


    /// <summary>
    ///   GradePointState Model , Represent the GradePointState Table in the base
    /// </summary>
    public abstract class GradePointState
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GradePointStateId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        [Display(Name = "Lower\nLimit")]
        public double LowerLimit { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        [Display(Name = "Upper\nLimit")]
        public double UpperLimit { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        [Display(Name = "Tuition\nRate\nFactor")]
        public double TuitionRateFactor { get; set; }


        [Display(Name = "State")]
        public string Description
        {
            get
            {
                return GetType().Name;

            }
        }

        //navigational Properties
        public virtual ICollection<Student> Student { get; set; }

    }


    /// <summary>
    /// SuspendedState Model , Represent the SuspendedState Table in the base
    /// </summary>
    public class SuspendedState : GradePointState
    {
        private static SuspendedState suspendedState { get; set; }
    }


    /// <summary>
    ///  ProbationState Model , Represent the  ProbationState Table in the base
    /// </summary>
    public class ProbationState : GradePointState
    {
        private static ProbationState probationState { get; set; }
    }

    /// <summary>
    /// RegularState Model , Represent the  RegularState Table in the base
    /// </summary>
    public class RegularState : GradePointState
    {
        private static RegularState regularState { get; set; }
    }

    /// <summary>
    /// HonoursState Model , Represent the HonoursState Table in the base
    /// </summary>
    public class HonoursState : GradePointState
    {
        private static HonoursState honoursState { get; set; }
    }

    /// <summary>
    /// Course Model , Represent the Course Table in the base
    /// </summary>
    public abstract class Course
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }


        [ForeignKey("AcademicProgram")]
        public int? AcademicProgramId { get; set; }

        [Required]
        [Display(Name = "Course\nNumber")]
        public string CourseNumber { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        [Display(Name = "Credit\nHours")]
        public double CreditHours { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:c2}")]
        [Display(Name = "Tuition")]
        public double TuitionAmount { get; set; }


        [Display(Name = "Course\nType")]
        public string CourseType
        {
            get
            {
                return GetType().Name;
            }
        }
        public string Notes { get; set; }


        //navigational Properties
        public virtual AcademicProgram AcademicProgram { get; set; }
        public ICollection<Registration> Registration { get; set; }
    }

    /// <summary>
    /// GradedCourse Model , Represent the GradedCourse Table in the base
    /// </summary>
    public class GradedCourse : Course
    {

        [Required]
        [Display(Name = "Assignments")]
        [DisplayFormat(DataFormatString = "{0:p2}")]
        public double AssignmentWeight { get; set; }

        [Required]
        [Display(Name = "Exams")]
        [DisplayFormat(DataFormatString = "{0:p2}")]
        public double Exameight { get; set; }
    }


    /// <summary>
    ///  MasteryCourse Model , Represent the  MasteryCourse Table in the base
    /// </summary>
    public class MasteryCourse : Course
    {
        [Required]
        [Display(Name = "Maximum\nAttempts")]
        public int MaximumAttempts { get; set; }
    }

    /// <summary>
    ///  AuditCourse Model , Represent the AuditCourse Table in the base
    /// </summary>
    public class AuditCourse : Course
    {

    }



}
