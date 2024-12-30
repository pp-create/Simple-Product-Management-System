using Simple_Product_Management_System.Dto;


namespace Simple_Product_Management_System.BLL.Product
{
    public interface IProdect
    {
        Task <IEnumerable<ProductDTO>> GetAll();
        Task<IEnumerable<ProductDTO>> Search(string search);
        Task<IEnumerable<ProductDTO>> Sort(string ColumName);
        Task<bool> Add(ProductDTO productDTO);
        Task<bool> Update(ProductDTO productDTO);
        Task<ProductDTO> GetByID(int Id);
        Task<bool> Delete(int Id);





    }
}
