import { Title } from "./components/title";
import { CharacterList } from "./components/character-list"

export default function Home() {
  return (
    <>
      <Title message="Star Wars API" />
      <CharacterList></CharacterList>
    </>
  );
}
