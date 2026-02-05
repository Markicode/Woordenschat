import { BrowserRouter, Routes, Route } from "react-router-dom";
import MainLayout from "../src/layouts/MainLayout";
import Home from "./views/Home";
import Login from "./views/Login";
import NotFound from "./views/NotFound";
import Books from "./views/Books";
import AddBook from "./views/AddBook";

function App() {
  return (
    <BrowserRouter>
      {/*<AuthProvider>*/}
      <Routes>
        {/* Layout levert altijd de context */}
        <Route path="/" element={<MainLayout />}>
          <Route index element={<Home />} />
          <Route path="login" element={<Login />} />
          <Route path="books" element={<Books />} />
          <Route path="addbook" element={<AddBook />} />

          {/* Routes die authentication + role check nodig hebben */}
          {/*<Route element={<PrivateRoute roles={["user", "admin"]} />}>
              <Route path="vessels" element={<Vessels />} />
              <Route path="products" element={<Products />} />
              <Route path="noauth" element={<NoAuth />} />
            </Route>

            <Route element={<PrivateRoute roles={["admin"]} />}>
              <Route path="members" element={<Members />} />
              <Route path="managevessels" element={<ManageVessels />} />
            </Route>
              */}
          <Route path="*" element={<NotFound />} />
        </Route>
      </Routes>
      {/*</AuthProvider>*/}
    </BrowserRouter>
  );
}

export default App;
