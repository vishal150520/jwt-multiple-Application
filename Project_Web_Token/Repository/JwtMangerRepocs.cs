namespace Project_Web_Token
{
    using Project_Web_Token.Tokens;
    using Project_Web_Token.Database;
    using Microsoft.AspNetCore.Mvc;

    public interface JwtMangerRepocs
    {
        tokens addAuthenticate(Validations validations);
        public TbEmployee RegisterForm(TbEmployee employee);
        
    }
}
