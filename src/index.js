import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { ErrorBoundary } from "react-error-boundary";
import { Provider } from "react-redux";
import { BrowserRouter } from "react-router-dom";
import store from "./stateManagement/store";
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <ErrorBoundary FallbackComponent={<>Loading...</>}>
    <Provider store={store}>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </Provider>
  </ErrorBoundary>
);

reportWebVitals();
