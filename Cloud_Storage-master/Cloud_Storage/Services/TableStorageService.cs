using Azure;
using Azure.Data.Tables;
using Cloud_Storage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TableStorageService
{
    private readonly TableClient _electronicTableClient;
    private readonly TableClient _applianceTableClient;
    private readonly TableClient _sightingTableClient;
    private readonly TableClient _toyTableClient;
    private readonly TableClient _customerTableClient;
    private readonly TableClient _productTableClient;

    public TableStorageService(string connectionString)
    {
        _electronicTableClient = new TableClient(connectionString, "Electronics");
        _applianceTableClient = new TableClient(connectionString, "Appliances");
        _sightingTableClient = new TableClient(connectionString, "Sighting");
        _toyTableClient = new TableClient(connectionString, "Toys");
        _customerTableClient = new TableClient(connectionString, "Customers");
        _productTableClient = new TableClient(connectionString, "Products");

        // Ensure the tables exist
        _electronicTableClient.CreateIfNotExists();
        _applianceTableClient.CreateIfNotExists();
        _sightingTableClient.CreateIfNotExists();
        _toyTableClient.CreateIfNotExists();
        _customerTableClient.CreateIfNotExists();
        _productTableClient.CreateIfNotExists();
    }

    // Electronics Methods
    public async Task AddElectronicsAsync(Electronics electronics)
    {
        if (string.IsNullOrEmpty(electronics.PartitionKey) || string.IsNullOrEmpty(electronics.RowKey))
        {
            throw new ArgumentException("PartitionKey and RowKey must be set.");
        }

        try
        {
            await _electronicTableClient.AddEntityAsync(electronics);
        }
        catch (RequestFailedException ex)
        {
            throw new InvalidOperationException("Error adding entity to Table Storage", ex);
        }
    }

    public async Task DeleteElectronicsAsync(string partitionKey, string rowKey)
    {
        await _electronicTableClient.DeleteEntityAsync(partitionKey, rowKey);
    }

    public async Task<Electronics?> GetElectronicsAsync(string partitionKey, string rowKey)
    {
        try
        {
            var response = await _electronicTableClient.GetEntityAsync<Electronics>(partitionKey, rowKey);
            return response.Value;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return null;
        }
    }

    public async Task<List<Electronics>> GetAllElectronicsAsync()
    {
        var electronicsList = new List<Electronics>();

        await foreach (var electronic in _electronicTableClient.QueryAsync<Electronics>())
        {
            electronicsList.Add(electronic);
        }

        return electronicsList;
    }

    // Appliances Methods
    public async Task<List<Appliances>> GetAllAppliancesAsync()
    {
        var appliancesList = new List<Appliances>();

        await foreach (var appliance in _applianceTableClient.QueryAsync<Appliances>())
        {
            appliancesList.Add(appliance);
        }

        return appliancesList;
    }

    public async Task AddApplianceAsync(Appliances appliance)
    {
        if (string.IsNullOrEmpty(appliance.PartitionKey) || string.IsNullOrEmpty(appliance.RowKey))
        {
            throw new ArgumentException("PartitionKey and RowKey must be set.");
        }

        try
        {
            await _applianceTableClient.AddEntityAsync(appliance);
        }
        catch (RequestFailedException ex)
        {
            throw new InvalidOperationException("Error adding entity to Table Storage", ex);
        }
    }

    public async Task DeleteApplianceAsync(string partitionKey, string rowKey)
    {
        await _applianceTableClient.DeleteEntityAsync(partitionKey, rowKey);
    }

    public async Task<Appliances?> GetApplianceAsync(string partitionKey, string rowKey)
    {
        try
        {
            var response = await _applianceTableClient.GetEntityAsync<Appliances>(partitionKey, rowKey);
            return response.Value;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return null;
        }
    }

    // Toys Methods
    public async Task<List<Toys>> GetAllToysAsync()
    {
        var toysList = new List<Toys>();

        await foreach (var toy in _toyTableClient.QueryAsync<Toys>())
        {
            toysList.Add(toy);
        }

        return toysList;
    }

    public async Task AddToyAsync(Toys toy)
    {
        if (string.IsNullOrEmpty(toy.PartitionKey) || string.IsNullOrEmpty(toy.RowKey))
        {
            throw new ArgumentException("PartitionKey and RowKey must be set.");
        }

        try
        {
            await _toyTableClient.AddEntityAsync(toy);
        }
        catch (RequestFailedException ex)
        {
            throw new InvalidOperationException("Error adding entity to Table Storage", ex);
        }
    }

    public async Task DeleteToyAsync(string partitionKey, string rowKey)
    {
        await _toyTableClient.DeleteEntityAsync(partitionKey, rowKey);
    }

    public async Task<Toys?> GetToyAsync(string partitionKey, string rowKey)
    {
        try
        {
            var response = await _toyTableClient.GetEntityAsync<Toys>(partitionKey, rowKey);
            return response.Value;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return null;
        }
    }

    // Sightings Methods
    public async Task AddSightingAsync(Sighting sighting)
    {
        if (string.IsNullOrEmpty(sighting.PartitionKey) || string.IsNullOrEmpty(sighting.RowKey))
        {
            throw new ArgumentException("PartitionKey and RowKey must be set.");
        }

        try
        {
            await _sightingTableClient.AddEntityAsync(sighting);
        }
        catch (RequestFailedException ex)
        {
            throw new InvalidOperationException("Error adding sighting to Table Storage", ex);
        }
    }

    public async Task<List<Sighting>> GetAllSightingsAsync()
    {
        var sightings = new List<Sighting>();

        await foreach (var sighting in _sightingTableClient.QueryAsync<Sighting>())
        {
            sightings.Add(sighting);
        }

        return sightings;
    }

    // Customer Methods
    public async Task AddCustomerAsync(Customer customer)
    {
        if (string.IsNullOrEmpty(customer.PartitionKey) || string.IsNullOrEmpty(customer.RowKey))
        {
            throw new ArgumentException("PartitionKey and RowKey must be set.");
        }

        try
        {
            await _customerTableClient.AddEntityAsync(customer);
        }
        catch (RequestFailedException ex)
        {
            throw new InvalidOperationException("Error adding customer to Table Storage", ex);
        }
    }

    public async Task DeleteCustomerAsync(string partitionKey, string rowKey)
    {
        await _customerTableClient.DeleteEntityAsync(partitionKey, rowKey);
    }

    public async Task<Customer?> GetCustomerAsync(string partitionKey, string rowKey)
    {
        try
        {
            var response = await _customerTableClient.GetEntityAsync<Customer>(partitionKey, rowKey);
            return response.Value;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return null;
        }
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        var customers = new List<Customer>();

        await foreach (var customer in _customerTableClient.QueryAsync<Customer>())
        {
            customers.Add(customer);
        }

        return customers;
    }

    // Product Methods
    public async Task AddProductAsync(Product product)
    {
        if (string.IsNullOrEmpty(product.PartitionKey) || string.IsNullOrEmpty(product.RowKey))
        {
            throw new ArgumentException("PartitionKey and RowKey must be set.");
        }

        try
        {
            await _productTableClient.AddEntityAsync(product);
        }
        catch (RequestFailedException ex)
        {
            throw new InvalidOperationException("Error adding product to Table Storage", ex);
        }
    }

    public async Task DeleteProductAsync(string partitionKey, string rowKey)
    {
        await _productTableClient.DeleteEntityAsync(partitionKey, rowKey);
    }

    public async Task<Product?> GetProductAsync(string partitionKey, string rowKey)
    {
        try
        {
            var response = await _productTableClient.GetEntityAsync<Product>(partitionKey, rowKey);
            return response.Value;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return null;
        }
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var products = new List<Product>();

        await foreach (var product in _productTableClient.QueryAsync<Product>())
        {
            products.Add(product);
        }

        return products;
    }
}
