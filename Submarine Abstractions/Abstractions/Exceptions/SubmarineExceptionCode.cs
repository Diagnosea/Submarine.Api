namespace Diagnosea.Submarine.Abstractions.Exceptions
{
    public enum SubmarineExceptionCode
    {
        ArgumentException,

        /// <summary>
        /// Unable to resolve an entity from the database.
        /// </summary>
        EntityNotFound
    }
}