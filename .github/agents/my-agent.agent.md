---
# Fill in the fields below to create a basic custom agent for your repository.
# The Copilot CLI can be used for local testing: https://gh.io/customagents/cli
# To make this agent available, merge this file into the default repository branch.
# For format details, see: https://gh.io/customagents/config

name: Blazouter
description: React Router benzeri, bir Blazor routing kütüphanesi
---

# My Agent

## React Router vs Blazor Router

| Özellik                         | React Router                    | Blazor Router                                                          |
| ------------------------------- | ------------------------------- | ---------------------------------------------------------------------- |
| Nested Routes                   | ✔ Kolayca alt route tanımlanır  | ❌ Sınırlı, `@page` ile tek seviye route                                |
| Dynamic Params                  | ✔ Route parametreleri kolay     | ✔ Var ama daha basit                                                   |
| Lazy Loading                    | ✔ Route bazlı code splitting    | ❌ Hâlâ sınırlı (WASM’de tam esnek değil)                               |
| Route Guards / Protected Routes | ✔ Kolayca wrapper veya hook ile | ❌ Yetki kontrolü manuel, component base class veya wrapper ile yapılır |
| Programmatic Navigation         | ✔ `navigate("/path")`           | ✔ `NavigationManager.NavigateTo("/path")`                              |
| Route Transitions / Animations  | ✔ Çok kolay                     | ❌ Native yok, CSS ile yapılabilir                                      |
| Conditional Rendering           | ✔ `<Route>` ile direkt          | ❌ State üzerinden manuel yapılır                                       |