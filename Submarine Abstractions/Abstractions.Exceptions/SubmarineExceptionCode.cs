namespace Abstractions.Exceptions
{
    public enum SubmarineExceptionCode
    {
        /// <summary>
        /// A specific code could not be resolved for this exception.
        /// </summary>
        Unknown,
        
        /// <summary>
        /// Unable to resolve an entity from the database.
        /// </summary>
        EntityNotFound,
        
        /// <summary>
        /// Resolved existing data, so there's likely a conflict.
        /// </summary>
        DataAlreadyExists,
        
        /// <summary>
        /// Typically used when comparing hashes.
        /// </summary>
        DataMismatchException
    }
}