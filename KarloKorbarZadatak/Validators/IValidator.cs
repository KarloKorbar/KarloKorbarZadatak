namespace KarloKorbarZadatak.Validators
{
    public interface IValidator<T> where T : class
    {
        public Boolean IsValid(T value);
    }
}
