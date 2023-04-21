import { get_api } from './Methods';
export function getCategories() {
 return get_api(`https://localhost:7281/api/categories`);

}

export function getAuthors() {
    return get_api(`https://localhost:7281/api/authors`);
   
}

