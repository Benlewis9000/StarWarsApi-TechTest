import axios from "axios";
import { Character } from "./character";

const baseUrl = "http://localhost:5000/api";

interface Response<T> {
    data: T | undefined,
    error: string
}

function getCharacters(): Promise<Response<Character[]>> {
    return axios.get<Character[]>(`${baseUrl}/character`)
    .then((response) => {
        return { data: response.data, error: "" };
    })
    .catch((error) => {
        return {data: undefined, error: error.message};
    })
    .finally(() => {
        return {data: undefined, error: "Unknown exception."};
    })
}

export { getCharacters };