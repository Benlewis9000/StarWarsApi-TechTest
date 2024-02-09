"use client"
import React, { useEffect, useState } from "react";
import { Character } from "../model/character";
import { getCharacters } from "../api/api-client";
import { CharacterRow } from "./character-row";

const CharacterList = () => {
    const [characters, setCharacters] = React.useState<Character[]>([]);
    const [isLoading, setIsLoading] = React.useState<boolean>(true);
    const [errorMessage, setErrorMessage] = React.useState<string>("");
    const [isErrored, setIsErrored] = React.useState<boolean>(false);
    
    useEffect(() => {
        setIsLoading(true);
        const fetchCharacters = async () => {
            const response = await getCharacters();
            //setIsLoading(false);
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
        <>
            <div className="row mb-2">
                <h5 className="themeFontColor text-center">
                    Characters
                </h5>
            </div>
            <div>
                <p>{isLoading}</p>
            {isLoading ?? <p>Loading...</p>}
            </div>
            {isErrored ? (
                <p>{errorMessage}</p>
                ) : (
                <table className="table table-hover">
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
            )}
        </>
    );
}

export { CharacterList }