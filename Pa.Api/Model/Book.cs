using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pa.Api.Controllers;

namespace Pa.Api;

public class Book
{
    
    [DisplayName("Book id")]
    public int Id { get; set; }
        
        
    
    [DisplayName("Book name")]
    public string Name { get; set; }
        
        
    
    [DisplayName("Book author info")]
    public string Author { get; set; }
        
        
    
    [DisplayName("Book page count")]
    public int PageCount { get; set; }
        
        
    
    [DisplayName("Book year")]
    public int Year { get; set; }

}