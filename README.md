# Soms.Shared.Cors

a lightweight library designed to simplify the configuration of Cross-Origin Resource Sharing (Cors) in .NET applications using appsettings.json.

## Usage Example

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

### Json Fields Mandatory and Default Values

| Property         | Mandatory | Default Value    |
| ---------------- | --------- | ---------------- |
| IsEnabled        | No        | `false`          |
| Name             | No        | `defaultPolicy`  |
| Origins          | Yes       | -                |
| Headers          | No        | `AllowAnyHeader` |
| Methods          | No        | `AllowAnyMethod` |
| ExposedHeaders   | No        | `None`           |
| AllowCredentials | No        | `None`           |

### 🚀 Semantic Versioning Rules (GitVersion + Conventional Commits)

| Commit Type                | Version Change                               |
| -------------------------- | -------------------------------------------- |
| To `Main` Branch           | Minor version bump (e.g., 1.2.0 → 1.3.0)     |
| To `Release/*` Branch      | Patch version bump (e.g., 1.2.0 → 1.2.1)     |
| git tag `2.0.0` or `3.0.0` | **Major** version bump (e.g., 1.2.0 → 2.0.0) |

# Thank You!
