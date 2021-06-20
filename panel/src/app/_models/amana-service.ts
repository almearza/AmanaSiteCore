export interface AmanaService {
    id: number;
    descr: string;
    link: string;
    imgUrl: string;
    typeId: number;
    typeName: string;
    uploadedDate: Date;
    uploadedBy: string;
    active: boolean;
    langCode: string,
    lang:string
}
export interface ServiceType {
    id: number;
    name: string;
}
