namespace NetCoreLibrary.Core.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }


        public  virtual int CustomerId { get; set; }
        /// <summary>
        /// Virtual neden verilir?
        /// EntityFramework ilgili customer'ı izleyebilsin(track edebilsin.)
        /// Insert, Update işlemlerini izleyebilirmesi için
        /// </summary>
        public virtual Customer Customer { get; set; }
    }
}
