

namespace Prn.Se1623;
public class Customer
{
    // Properties (not field)
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public Customer() { }

    public Customer(string id, string name, string address, string phone)
    {
        Id = id;
        Name = name;
        Address = address;
        Phone = phone;
    }



    public override string? ToString()
    {
        return $"[Id = {this.Id}, Name = {this.Name}, Adress = {this.Address}, PHone = {this.Phone}]";
    }


}

