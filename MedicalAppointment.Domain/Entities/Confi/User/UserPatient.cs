﻿

using MedicalAppointment.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointment.Domain.Entities.Confi.User
{
    public sealed class UserPatient : BaseEntity
    {
        [Key]
        public int PatiendID { get; set; }
        public DateTime DateofBirth { get; set; }   
        public char Gender { get; set; }    
        public string Address { get; set; }  
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
        public string Allergies { get; set; }
        public int InsuranceProviderID { get; set; }    

    }
}
        