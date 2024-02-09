import { Title } from "./title";
import { CharacterList } from "./character-list"

export default function Home() {
  return (
    <>
      <Title message="Star Wars API" />
      <CharacterList></CharacterList>
    </>
  );
}
