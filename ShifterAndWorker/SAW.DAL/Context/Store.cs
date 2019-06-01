//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAW.DAL.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            this.Departments = new HashSet<Department>();
            this.Roles = new HashSet<Role>();
            this.Users = new HashSet<User>();
            this.WorkingTypes = new HashSet<WorkingType>();
        }
    
        public int StoreId { get; set; }
        public string StoreCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<byte> MaxNumberEmployees { get; set; }
        public Nullable<byte> MinNumberEmployees { get; set; }
        public Nullable<System.TimeSpan> OpeningTime { get; set; }
        public Nullable<System.TimeSpan> ClosingTime { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int AddressId { get; set; }
        public int CompanyId { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Company Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Departments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role> Roles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkingType> WorkingTypes { get; set; }
    }
}
