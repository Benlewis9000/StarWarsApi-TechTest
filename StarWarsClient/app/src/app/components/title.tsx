import React from "react";
import Image from "next/image";

interface TitleProps {
    message: string;
}

const Title: React.FC<TitleProps> = ({message}) => {
    return (
        <div className="container text-center">
            <div className="col">
                <Image src="star-wars.svg" alt="logo" width="200" height="200"/>
            </div>
            <div className="col h1">
                {message}
            </div>
        </div>
    );
};

export { Title };