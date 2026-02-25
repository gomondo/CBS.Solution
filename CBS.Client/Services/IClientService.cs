namespace CBS.Client.Services
{
    public interface IClientService<T>
    {
        /// <summary>API endpoint path, e.g. "api/clinic"</summary>
        string EndPoint { get; set; }

        /// <summary>Last HTTP-level error message, if any.</summary>
        string HttpErrorMessage { get; set; }

        /// <summary>Returns all records.</summary>
        Task<IEnumerable<T>> List();

        /// <summary>Finds a single record by Id.</summary>
        Task<T> Find(long id);

        /// <summary>
        /// Validates then POSTs a new record.
        /// Returns (true, "Created.") on success, or (false, "error details") on failure.
        /// </summary>
        Task<KeyValuePair<bool, string>> AddNew(T model);

        /// <summary>
        /// Validates then PUTs an updated record.
        /// Returns (true, "Updated.") on success, or (false, "error details") on failure.
        /// </summary>
        Task<KeyValuePair<bool, string>> Edit(long id, T model);

        /// <summary>
        /// Deletes a record by Id.
        /// Returns (true, "Deleted.") on success, or (false, "error details") on failure.
        /// </summary>
        Task<KeyValuePair<bool, string>> Delete(long id);
    }
}
