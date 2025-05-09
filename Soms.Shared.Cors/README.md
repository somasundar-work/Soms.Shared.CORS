# Soms.Shared.Cors
a lightweight library designed to simplify the configuration of Cross-Origin Resource Sharing (Cors) in .NET applications using appsettings.json.

## Usage

### To Configure Cors
```csharp
builder.Services.ConfigureAppCors();
```

### To Use Cors
```csharp
app.UseAppCors();
```

### Data for Cors in Appsettings.json File

```json
{
    "cors": {
        "IsEnabled": true,
        "Name": "MyCorsPolicy",
        "Origins": ["example.com", "api.com"],
        "Headers": ["Accept", "Authorization", "X-Api-Version", "X-Correlation-Id"],
        "Methods": ["GET", "POST", "PATCH"],
        "ExposedHeaders": ["X-Pagination", "X-Correlation-Id", "X-Api-Version"],
        "AllowCredentials": true
    }
}
```

Note: Mandatory fields are "IsEnabled" and "Origins". below example json will also work. 

```json
{
    "cors": {
        "IsEnabled": true,
        "Origins": ["example.com", "api.com"],
    }
}
```

### default values for not configured Fields

| Property         | Default Value     |
|------------------|-------------------|
| Name             | `defaultPolicy`   |
| Headers          | `AllowAnyHeader`  |
| Methods          | `AllowAnyMethod`  |
| ExposedHeaders   | `None`            |
| AllowCredentials | `None`            |

# Thank You!