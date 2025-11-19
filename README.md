# dotnet-lib-template

A clean and minimal **template repository** for .NET libraries.  
It provides a consistent project structure, CI/CD setup, and build configuration, so you can quickly start new libraries without repeating boilerplate.

---

## 📂 Project Structure
- `src/` – main library code
- `tests/` – unit tests (xUnit)
- `samples/` – sample usage (console app)
- `.github/workflows/ci.yml` – GitHub Actions workflow (build + test)
- `Directory.Build.props` – shared build settings (nullable enabled, warnings as errors, etc.)

---

## 🚀 Getting Started

1. Click **Use this template** on GitHub.
2. Create a new repository for your library (e.g., `my-library`).
3. Clone the new repository locally:
   ```bash
   git clone https://github.com/<your-org>/<your-repo>.git
   cd <your-repo>
