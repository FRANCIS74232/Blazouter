# Contributing to Blazouter

Thank you for your interest in contributing to Blazouter! We welcome contributions from the community.

## How to Contribute

### Reporting Issues

If you find a bug or have a feature request:

1. Check if the issue already exists in the [issue tracker](https://github.com/Taiizor/Blazouter/issues)
2. If not, create a new issue with:
   - Clear title and description
   - Steps to reproduce (for bugs)
   - Expected vs actual behavior
   - Your environment details (OS, .NET version, browser)

### Pull Requests

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Make your changes
4. Add or update tests if applicable
5. Update documentation as needed
6. Commit your changes (`git commit -m 'Add amazing feature'`)
7. Push to the branch (`git push origin feature/amazing-feature`)
8. Open a Pull Request

### Development Setup

```bash
# Clone the repository
git clone https://github.com/Taiizor/Blazouter.git
cd Blazouter

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run a sample application (choose one):
# For Server sample:
cd samples/Blazouter.Server.Sample
dotnet run

# For WebAssembly sample:
# cd samples/Blazouter.WebAssembly.Sample
# dotnet run

# For Hybrid sample (MAUI):
# cd samples/Blazouter.Hybrid.Sample
# dotnet build -t:Run -f net9.0-windows10.0.19041.0

# For Web sample (.NET 8+ with Server + WASM):
# cd samples/Blazouter.Web.Sample/Blazouter.Web.Sample
# dotnet run
```

### Coding Guidelines

- Follow C# coding conventions
- Write clear, self-documenting code
- Add XML documentation comments for public APIs
- Keep methods focused and concise
- Write unit tests for new features

### Commit Messages

- Use clear and descriptive commit messages
- Start with a verb in present tense (Add, Fix, Update, Remove)
- Reference issue numbers when applicable

Example:
```
Add lazy loading support for nested routes (#123)

- Implement ComponentLoader functionality
- Add loading state management
- Update documentation
```

## Code of Conduct

Please be respectful and constructive in all interactions. We're here to build great software together!

## Questions?

Feel free to open an issue for questions or join discussions in the repository.

Thank you for contributing! 🚀