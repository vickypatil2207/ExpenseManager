# ExpenseManager Debugging Guide

## Quick Start

Press **F5** to start debugging.

### Configuration Options:

1. **API + Web (Full Stack)** ← Start here!
   - Runs API with debugger attached ✅
   - Runs Blazor Web app in the background
   - Opens browser automatically
   - API breakpoints work in VS Code

2. **API Only (Quick Debug)**
   - Runs only the API with debugger
   - Good for isolated API testing

---

## Debugging API (.NET Backend)

✅ **API breakpoints work directly in VS Code**

1. Select **"API + Web (Full Stack)"** from Debug dropdown
2. Press **F5**
3. Set breakpoints in any `.cs` file in `ExpenseManager.Api/`
4. Make API requests from the Blazor web app
5. Breakpoints will be **solid red** and execution will pause ✅

---

## Debugging Blazor Web (WASM Frontend)

Since Blazor WebAssembly runs in the browser, use the browser's DevTools:

### Option A: Browser DevTools (Recommended)
1. **F5** → Select **"API + Web (Full Stack)"** → Start debugging
2. Browser opens at `http://localhost:5112`
3. In the browser, press **F12** to open Developer Tools
4. Go to **Sources** tab
5. Find your `.razor.cs` files (use Ctrl+P to search)
6. Set breakpoints directly in the browser
7. Navigate the app to trigger your breakpoints ✅

### Option B: Console Debugging
1. Open browser DevTools (F12)
2. Go to **Console** tab
3. Add `console.log()` statements in your Blazor components
4. Check the console output as you navigate

---

## Debugging Both Simultaneously

1. Press **F5** → **"API + Web (Full Stack)"**
2. VS Code watches API code
3. Browser DevTools watches Blazor code
4. Set breakpoints in both places
5. Debug your full stack! 🚀

---

## Troubleshooting

**API breakpoints not hitting?**
- Make sure you selected **"API (.NET)"** or **"API + Web (Full Stack)"** 
- Rebuild with `dotnet build`
- Restart debugging with F5

**Blazor app not loading?**
- Check browser console (F12) for errors
- Verify API is running on `http://localhost:5051`
- Check `wwwroot/appsettings.json` has correct `ApiBaseUrl`

**API and Blazor won't start together?**
- Try **"API Only"** first to verify API works
- Then run **"API + Web"** for both

---

## URL Reference

- **API**: `http://localhost:5051` (HTTP only)
- **Blazor Web**: `http://localhost:5112`
- **API Swagger**: `http://localhost:5051/swagger`
