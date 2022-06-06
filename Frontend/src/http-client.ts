import axios from "axios";

export const httpClient = axios.create({
    baseURL: "https://localhost:7139/api/v1.0",
    headers: {
        "Content-type": "application/json"
    }
});

export default httpClient

