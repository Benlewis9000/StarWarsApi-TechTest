import React from "react"
import { Character } from "../model/character";

interface CharacterItemProps {
    key: string,
    character: Character
}

const CharacterRow: React.FunctionComponent<CharacterItemProps> = ({key, character}) => {
    console.log(character);
    return (
        <tr key={key}>
            <td>{character.name}</td>
            <td>{character.birthYear}</td>
            <td>{character.homeWorld}</td>
            <td>{character.filmCount}</td>
        </tr>
    )
}

export { CharacterRow }