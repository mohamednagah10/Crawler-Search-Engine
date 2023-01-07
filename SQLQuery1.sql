select sum(freq) from postings where dic_dic_id=144
select (a.e-d.freq) q ,d.dic_id from (select sum(s.freq) e,s.dic_dic_id from postings s  group by s.dic_dic_id) a inner join dics d on a.dic_dic_id=d.dic_id where (a.e-d.freq)>0 or (a.e-d.freq)>0
select postions.id ,postings.id from postions right join postings on postions.posting_id=postings.id and postings.dic_dic_id=1895  where postings.dic_dic_id=1895  and posting_id is null
select sum(freq) from postings where dic_dic_id=144 


select s.id,s.freq,b.v ,(s.freq - b.v) from postings s inner join (select count(*) v,posting_id from postions inner join postings on postions.posting_id=postings.id and postings.dic_dic_id=1895  where postings.dic_dic_id=1895 group by postions.posting_id )b on s.id=b.posting_id  where s.dic_dic_id=1895  
