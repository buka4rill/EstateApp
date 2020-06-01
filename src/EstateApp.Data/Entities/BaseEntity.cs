using System;

namespace EstateApp.Data.Entities
{
    // Generic Properties I want my Entity Classes to have
    // Can't create an new instance of this class
    // but can inherit from it
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}