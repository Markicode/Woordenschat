import axios from "axios";

export const api = axios.create({
  baseURL: "https://192.168.2.4:7136/api"
});