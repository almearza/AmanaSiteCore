export interface NewsTypes {
    id: number;
    type: string;
}

export interface News {
    id: 0;
    title: string;
    descr: string;
    imgUrl: string;
    typeName: string;
    typeId: number;
    newsDate: Date;
    uploadedBy: string;
    active: boolean;
    newsResource: string;
    lang: string;
    langCode:number;
}