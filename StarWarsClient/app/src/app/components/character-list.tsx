"use client"
import React, { useEffect, useState } from "react";
import { Character } from "../model/character";
import { getCharacters } from "../api/api-client";
import { CharacterRow } from "./character-row";

const CharacterList = () => {
    const [characters, setCharacters] = useState<Character[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [errorMessage, setErrorMessage] = useState<string>("");
    const [isErrored, setIsErrored] = useState<boolean>(false);
    
    useEffect(() => {
        const fetchCharacters = async () => {
            const response = await getCharacters();
            setIsLoading(false);
            if (response.data === undefined){
                setIsErrored(true);
                setErrorMessage(response.error);
            }
            else {
                setCharacters(response.data);
            }
        }

        fetchCharacters();
    }, [])

    return (
        <div className="shadow d-flex flex-column justify-content-center text-center align-items-center">
            <div className="m-2">
                <h3 className=" themeFontColor">
                    Characters
                </h3>
            </div>
            <div className="mb-3">
                {isLoading ? (
                    <div className="alert alert-light">Loading...</div>
                ) : (
                    isErrored ? (
                        <div className="alert alert-danger">{errorMessage}</div>
                        ) : (
                        <table className="table table-striped table-hover shadow text-start">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Birth Year</th>
                                    <th>Homeworld</th>
                                    <th>Film Appearances</th>
                                </tr>
                            </thead>
                            <tbody>
                                {isLoading ?? <p>Loading...</p>}
                                {isErrored ? (
                                    <p>{errorMessage}</p>
                                ) : (
                                    characters.map(character => <CharacterRow key={character.name} character={character} />)
                                )}
                            </tbody>
                        </table>
                    )
                )}
            </div>
        </div>
    );
}

export { CharacterList }