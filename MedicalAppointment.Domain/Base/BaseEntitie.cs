

namespace Medical.Domain.Base
{
    public abstract class BaseEntitie
    {

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime?  UpdatedAt { get; set; } 

    }

}
 
